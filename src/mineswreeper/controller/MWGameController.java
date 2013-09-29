package mineswreeper.controller;

import mineswreeper.model.MWGameModel;
import mineswreeper.view.MWFrameView;

/** Classe de controleur d'une partie de démineur fait le lien entre la vue et le modèle
 * @author Camier Mickael
 *
 */
public class MWGameController {
 
	/** Instance de modèle rattaché au controleur (une seule)
	 */
	private MWGameModel model;
	/** Instance de vue rattachée au controleur (mais plusieurs possibles)
	 */
	private MWFrameView view;
	//private MWFrameView view2;
	
	
	/** Constructeur de controleur de jeu démineur
	 * @param model Modèle unique rattaché au controleur
	 */
	public MWGameController(MWGameModel model)
	{
		this.model = model;
		this.view = new MWFrameView(this, this.model.getWidth(), this.model.getHeight());
		//this.view2 = new MWFrameView(this, this.model.getWidth(), this.model.getHeight());
		model.addMWGameListener(this.view);
		//model.addMWGameListener(this.view2);
	}
	
	/** Affiche l'ensemble des vues du controleur
	 */
	public void displayView() 
	{
		view.display();
		//view2.display();
	}
	
	
	/** Ferme l'ensemble des vues du controleur
	 */
	public void closeView() 
	{
		view.close();
		//view2.close();
	}
	
	/** Reset le modèle (cad relance une partie)
	 */
	public void notifyReset() 
	{
		this.model.reset();
	}
	
	/** Reset le modèle (cad relance une partie)
	 */
	public void notifyReset(int width, int height, int nbMines) 
	{
		this.model.reset(width, height, nbMines);
	}
	
	/** Notifie une clique gauche sur une case au modèle
	 * @param idCase ID de la case cliquée
	 */
	public void notifyCellClicked(int idCase)
	{
		this.model.reveal(idCase);
	}
	
	
	/** Notifie une clique droit sur une case au modèle
	 * @param idCase ID de la case cliquée
	 */
	public void notifyCellMarked(int idCase)
	{
		this.model.mark(idCase);
	}
	
	
	/** Notifie une clique milieu sur une case au modèle
	 * @param idCase ID de la case cliquée
	 */
	public void notifyCellHelp(int idCase)
	{
		this.model.help(idCase);
	}
	
	public int getNbMines()
	{
		return this.model.getNbMines();
	}
	
	
	public int getNbCellMarked()
	{
		return this.model.getNbCellMarked();
	}
}
