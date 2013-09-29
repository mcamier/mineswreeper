package mineswreeper.model;

import java.util.EventObject;

/**
 * @author Fufuuu
 *
 */
@SuppressWarnings("serial")
public class MWGameUpdateEvent extends EventObject {

	/**
	 * @param source
	 */
	public MWGameUpdateEvent(Object source) {
		super(source);
	}

}
