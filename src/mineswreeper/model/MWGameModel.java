package mineswreeper.model;

import java.util.ArrayList;
import javax.swing.event.EventListenerList;

import java.util.Random;

/** Classe de modèle d'une partie de démineur, contient l'ensemble des informations persistantes d'une partie ainsi que les méthodes de manipulation de ces dernières
 * @author Camier Mickael
 */
public class MWGameModel {
	/** Largeur en cases
	 */
	private int width;
	/** Hauteur en cases
	 */
	private int height;
	/** Nombre de mines
	 */
	private int nbMines;
	/** Nombre de case
	 */
	private int nbMinesMarked;
	private int nbCellRevealed;
	private int nbCellMarked;
	private int nbCells;
	/** Il s'agit de la grille de jeu, elle n'est pas stocké sur un tableau deux dimensions pour faciliter son utilisation
	 * mm si cela parait moins naturel, c'est relativement simple :
	 * on part du principe que chaques cases possède un ID allant de O à nbCases-1
	 * si je fais ID%width j'obtiens l'abscisse de la représentation en tableau
	 * si je fais ID/width j'obtiens l'ordonné de la représentation en tableau
	 * tableau indicé de 0 a width-1 et 0 à height-1
	 * 
	 * Les coordonnées ne sont utilisés que pour atteindre les cases aux alentours d'une autres
	 */
	private ArrayList<MWCell> cells;
	/** List d'écouteurs d'event sur le modèle, cad des objet qui implémente EventListener
	 */
	private EventListenerList listeners;
		

	/** Constructeur du modèle, initialise les attributs et appele la méthode d'initialisation de la "grille"
	 * @param width Largeur de la zone de jeu
	 * @param height Hauteur de la zone de jeu
	 * @param nbMines Nombre de mines de la zone de jeu
	 */
	public MWGameModel(int width, int height, int nbMines)
	{
		this.width = width;
		this.height = height;
		this.nbCells = width * height;
		this.nbMines = nbMines;
		this.nbMinesMarked = 0;
		this.nbCellMarked = nbMines;
		this.nbCellRevealed = 0;
		this.cells = new ArrayList<MWCell>();
		this.listeners = new EventListenerList();
		
		this.InitGrid(width*height);
	}
	
	
	/** Initialise la grille (pas visuellement)
	 * @param num Nombre de cases de la partie 
	 */
	private void InitGrid(int num) 
	{
		// Mémorisation des cases libres dans cette arrayList, utile pour sa commodité, je le remplis de type Integer
		// car une arrayList de type primitif int est impossible
		ArrayList<Integer> temp = new ArrayList<Integer>();
		for(int i=0 ; i < num; i++) 
		{
			this.cells.add(new MWEmptyCell(i));
			temp.add(i);
		}
		
		Random randomizer = new Random();
		for(int i=0 ; i < nbMines ; i++)
		{
			int size = temp.size();
			int rInt = randomizer.nextInt(size);
			this.cells.set(temp.get(rInt), new MWMineCell(temp.get(rInt)));
			this.updateNeightbours(temp.get(rInt));
			temp.remove(rInt);
		}
	}
	
	
	/** Retourne la largeur de la grille de jeu
	 * @return int
	 */
	public int getWidth() 
	{
		return this.width;
	}
	
	
	/** Retourne la hauteur de la grille de jeu
	 * @return int
	 */
	public int getHeight() 
	{
		return this.height;
	}
	
	/** Retourne le nombre de mines de la grille de jeu
	 * @return int
	 */
	public int getNbMines() 
	{
		return this.nbMines;
	}
	
	
	/** Méthode appelé par le controleur lorsqu'un clic gauche est effectué sur une case, le paramètre id est l'ID du la case, correspondant à l'indice de MWCell correspondant dans l'arrayList
	 * @param id ID de la MWCell a mettre a jour
	 */
	public void reveal(int id)
	{
		if(this.getCase(id).isMined()) {
			// en cas de clic sur un case minée, lancement de la gestion de la défaite
			this.lostGameToListeners(id);
		}
		else {
			if( !this.getCase(id).known )
			{
				if(this.getCase(id).marked) this.getCase(id).marked = false;
				if(this.getCase(id).neightbours == 0) 
				{
					this.floodFill(id);
				}
				else 
				{
					this.nbCellRevealed++;
					this.getCase(id).known = true;
					this.cellUpdateToListeners(id);
				}
			}
		}
		this.isWon();
	}
	
	
	/** Méthode appelé par le controleur lorsqu'un clic droit est effectué sur une case, le paramètre id est l'ID du la case, correspondant à l'indice de MWCell correspondant dans l'arrayList
	 * @param id ID de la MWCell a mettre a jour
	 */
	public void mark(int id)
	{
		MWCell cell = this.getCase(id);
		if(!cell.known) 
		{
			if(cell.marked)
			{
				if(cell.isMined()) this.nbMinesMarked--;
				cell.marked = false;
				this.nbCellMarked++;
			}
			else 
			{
				if(cell.isMined()) this.nbMinesMarked++;
				cell.marked = true;
				this.nbCellMarked--;
			}
		}
		this.cellUpdateToListeners(id);
		this.isWon();
	}
	
	
	public void isWon() 
	{
		if ( ( this.nbMinesMarked == this.nbMines) && ((this.nbCellRevealed+this.nbMinesMarked)==this.nbCells) ) 
		{
			this.wonGameToListeners();
			System.out.println("Victory");
		}
	}
	
	
	public void floodFill(int id) 
	{
		MWCell cell = this.getCase(id);
		if( (!cell.isMined()) && (!cell.known) )
		{
			this.nbCellRevealed++;
			cell.known = true;
			if(cell.neightbours == 0) {
				if(cell.marked) cell.marked = false;
				this.cellUpdateToListeners(id);
				int tempId;
				if( (tempId=this.getUpCaseId(id)) != -1 ) this.floodFill(tempId);
				if( (tempId=this.getDownCaseId(id)) != -1 ) this.floodFill(tempId);
				if( (tempId=this.getRightCaseId(id)) != -1 ) this.floodFill(tempId);
				if( (tempId=this.getLeftCaseId(id)) != -1 ) this.floodFill(tempId);
			}
			else {
				this.cellUpdateToListeners(id);
			}
		}
	}
	
	/** Reset la partie (cad réinitialise les données pour en faire une nouvelle partie)
	 */
	public void help(int id) 
	{
		MWCell cell = this.getCase(id);
		if(!cell.isMined())
		{
			this.nbCellRevealed++;
			int tempId;
			cell.known = true;
			if(cell.neightbours > 0) {
				if(cell.marked) cell.marked = false;
				this.cellUpdateToListeners(id);
				if( (tempId=this.getUpCaseId(id)) != -1 ) this.help(tempId);
				if( (tempId=this.getDownCaseId(id)) != -1 ) this.help(tempId);
				if( (tempId=this.getRightCaseId(id)) != -1 ) this.help(tempId);
				if( (tempId=this.getLeftCaseId(id)) != -1 ) this.help(tempId);
			}
			else {
				this.cellUpdateToListeners(id);
			}
		}
	}
	
	/** Reset la partie (cad réinitialise les données pour en faire une nouvelle partie)
	 */
	public void reset() 
	{
		this.reset(this.width, this.height, this.nbMines);
	}
	
	
	public void reset(int width, int height, int nbMines) 
	{
		this.width = width;
		this.height = height;
		this.nbMines = nbMines;
		this.nbMinesMarked = 0;
		this.nbCellRevealed = 0;
		this.nbCellMarked = nbMines;
		this.nbCells = width*height;
		this.cells.clear();
		this.InitGrid(width*height);
		this.resetGameToListeners(width*height);
	}
	
	
	/** Incrémente la valeur neightbours des 8 cases autour de celle dont l'id est passé à cette fonction
	 * @param id ID de la case de référence
	 */
	private void updateNeightbours(int id) 
	{
		int cell;
		if( (cell=this.getDownCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getUpCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getRightCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getLeftCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getDownLeftCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getDownRightCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getUpLeftCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();
		if( (cell=this.getUpRightCaseId(id)) != -1) this.getCase(cell).increaseNeightboursValue();		
	}
	

	public int getNbCellMarked()
	{
		return this.nbCellMarked;
	}
	
	
	/** Retourne la case supérieur si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getUpCaseId(int id) 
	{
		return (id-this.width >= 0) ? id-this.width : -1;
	}
	
	
	/** Retourne la case inférieur si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getDownCaseId(int id) 
	{
		return (id+this.width < this.nbCells) ? id+this.width : -1;
	}
	
	
	/** Retourne la case droite si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getRightCaseId(int id) 
	{
		int idDest=id+1;
		return (( (id/this.width) == ((idDest)/this.width) ) && (idDest<this.nbCells)) ? id+1 : -1;
	}
	
	
	/** Retourne la case gauche si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getLeftCaseId(int id) 
	{
		int idDest=id-1;
		return (( (id/this.width) == ((idDest)/this.width) ) && (idDest>=0)) ? id-1 : -1;
	}
	
	
	/** Retourne la case supérieur droite si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getUpRightCaseId(int id) 
	{
		int idDest=id-this.width+1;
		return (( (id/this.width)-1 == ((idDest)/this.width) ) && (idDest>=0)) ? id-this.width+1 : -1;
	}
	
	
	/** Retourne la case supérieur gauche si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getUpLeftCaseId(int id) 
	{
		int idDest=id-this.width-1;
		return (( (id/this.width)-1 == ((idDest)/this.width) ) && (idDest>=0)) ? id-this.width-1 : -1;
	}
	
	
	/** Retourne la case inférieur droite si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getDownRightCaseId(int id) 
	{
		int idDest=id+this.width+1;
		return (( (id/this.width)+1 == ((idDest)/this.width) ) && (idDest<this.nbCells)) ? id+this.width+1 : -1;
	}
	
	
	/** Retourne la case inférieur gauche si existante, sinon null
	 * @param id ID de la case de référence
	 * @return MWCell
	 */
	public int getDownLeftCaseId(int id) 
	{
		int idDest=id+this.width-1;
		return (( (id/this.width)+1 == ((idDest)/this.width) ) && (idDest<this.nbCells)) ? id+this.width-1 : -1;
	}
	

	/** Retourne l'instance MWCell ayant l'ID demandé
	 * @param id ID de la case demandé
	 * @return MWCell
	 */
	public MWCell getCase(int id) 
	{
		return cells.get(id);
	}
	
	
	/**
	 * @param listener
	 */
	public void addMWGameListener(MWGameListener listener) 
	{
		this.listeners.add(MWGameListener.class, listener);
	}
	
	
	/**
	 * @param listener
	 */
	public void removeMWGameListener(MWGameListener listener) 
	{
		this.listeners.remove(MWGameListener.class, listener);
	}
	
	
	/**
	 * @param nbCell
	 */
	public void resetGameToListeners(int nbCell) 
	{
		MWGameListener[] listenerList = (MWGameListener[])listeners.getListeners(MWGameListener.class);
		
		for(MWGameListener listener : listenerList){
			listener.gameReset(new MWGameUpdateEvent(this), width, height);
		}
	}
	
	
	/**
	 * @param id
	 */
	public void lostGameToListeners(int id) 
	{
		MWGameListener[] listenerList = (MWGameListener[])listeners.getListeners(MWGameListener.class);
		
		for(MWGameListener listener : listenerList){
			for(MWCell cell : this.cells)
				this.cellUpdateToListeners(cell.getID());
			listener.gameLost(new MWGameCellUpdateEvent(this, this.cells.get(id)));
		}
	}
	
	public void wonGameToListeners()
	{
		MWGameListener[] listenerList = (MWGameListener[])listeners.getListeners(MWGameListener.class);
		
		for(MWGameListener listener : listenerList){
			listener.gameWon(new MWGameUpdateEvent(this));
		}
	}
	
	
	/**
	 * @param id
	 */
	public void cellUpdateToListeners(int id) 
	{
		MWGameListener[] listenerList = (MWGameListener[])listeners.getListeners(MWGameListener.class);
		
		for(MWGameListener listener : listenerList){
			listener.updateGrid(new MWGameCellUpdateEvent(this, this.cells.get(id)));
		}
	}

	
	/** Classe interne abstraite MWCell
	 *
	 * @author Fufuuu
	 *
	 */
	public abstract class MWCell
	{
		boolean known;
		boolean marked;
		final int ID;
		int neightbours;
		
		public MWCell(int id) 
		{
			this.ID = id;
			this.known = false;
			this.marked = false;
			this.neightbours = 0;
		}
		
		public void toggleMark()
		{
			if(!this.known) 
				this.marked = !this.marked;
		}
		
		public void setKnown(boolean b)
		{
			this.known = b;
		}
		
		public int getID()
		{
			return this.ID;
		}
		
		public abstract ContextCell getContext();
		public abstract void increaseNeightboursValue();
		public abstract boolean isMined();
		
		public String toString()
		{
			return " : " + ID + " ; Known? " + known + " ; Marked? " + marked;
		}
	}
	
	
	/** Classe interne MWEmptyCell héritant de MWCell
	 *
	 * @author Fufuuu
	 *
	 */
	public class MWEmptyCell extends MWCell 
	{
		public MWEmptyCell(int id) 
		{
			super(id);
		}
		
		public String toString()
		{
			return "Case normal" + super.toString();
		}

		public ContextCell getContext() 
		{
			return (this.marked) ? ContextCell.marked : ContextCell.values()[this.neightbours];
		}
		
		public boolean isMined() 
		{
			return false;
		}
		
		public void increaseNeightboursValue() 
		{
			this.neightbours++;
		}
	}
	
	
	/** Classe interne MWMineCell héritant de MWCell
	 *
	 * @author Fufuuu
	 *
	 */
	public class MWMineCell extends MWCell 
	{
		public MWMineCell(int id)
		{
			super(id);
		}
		
		public String toString()
		{
			return "Case miné" + super.toString();
		}
		
		public ContextCell getContext() 
		{
			return (this.marked) ? ContextCell.marked : ContextCell.mined;
		}
		public boolean isMined() 
		{
			return true;
		}
		
		public void increaseNeightboursValue(){}
	}
	
	
	/** Enum décrivant l'état d'une case 
	 *
	 * @author Fufuuu
	 *
	 */
	public static enum ContextCell {
		empty, oneNeightbor, twoNeightbor, threeNeightbor, forNeightbor, fiveNeightbor, sixNeightbor, sevenNeightbor, eightNeightbor, unknowned, mined, wrong, marked
	}
}
