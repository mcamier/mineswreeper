package mineswreeper;

import mineswreeper.model.MWGameModel;
import mineswreeper.controller.MWGameController;

/** Classe de lancement du jeu de démineur
 * @author Camier Mickael
 *
 */
public class MWGame {

	/** Launcher du jeu
	 * @param args
	 */
	public static void main(String[] args) 
	{
		MWGameModel model = new MWGameModel(25, 25, 40);
		MWGameController controller = new MWGameController(model);
		controller.displayView();
	}

}
