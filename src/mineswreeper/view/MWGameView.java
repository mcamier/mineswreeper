package mineswreeper.view;

import mineswreeper.controller.MWGameController;
import mineswreeper.model.MWGameListener;

/** Classe abstraite que doit étendre une classe de vue de démineur
 * @author Camier Mickael
 *
 */
public abstract class MWGameView implements MWGameListener {
	
	/** Controleur rattaché à la vue
	 */
	private MWGameController controller;

	
	/** Constructeur de classe de vue 
	 * @param controller Controleur a rattacher à la classe de vue
	 */
	public MWGameView(MWGameController controller)
	{
		this.controller = controller;
	}

	
	/** Retourne le controleur rattaché à la vue
	 * @return le controleur rattaché à la vue
	 */
	public MWGameController getController() 
	{
		return this.controller;
	}

	
	/** Affiche la vue
	 */
	public abstract void display();
	
	/** Masque la vue
	 */
	public abstract void close();
}