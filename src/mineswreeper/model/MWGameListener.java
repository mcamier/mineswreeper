package mineswreeper.model;

import java.util.EventListener;
 
/** Interface d'écoute d'event du démineur
 * @author Camier Mickael
 *
 */
public interface MWGameListener extends EventListener {
	
	/**
	 * @param event Evènement sur case du jeu
	 */
	public void updateGrid(MWGameCellUpdateEvent event);
	
	/**
	 * @param event Evènement par défaut du démineur
	 */
	public void gameWon(MWGameUpdateEvent event);
	
	/** 
	 * @param event Evènement sur case du jeu
	 */
	public void gameLost(MWGameCellUpdateEvent event);
	
	/**
	 * @param event Evènement par défaut du démineur
	 * @param width Largeur a utiliser pour la partie "resetée"
	 * @param height Hauterur a utiliser pour la partie "resetée"
	 */
	public void gameReset(MWGameUpdateEvent event, int width, int height);
}
