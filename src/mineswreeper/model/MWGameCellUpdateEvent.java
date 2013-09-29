package mineswreeper.model;

import java.util.EventObject;

import mineswreeper.model.MWGameModel.MWCell;

/**
 * @author Fufuuu
 *
 */
@SuppressWarnings("serial")
public class MWGameCellUpdateEvent extends EventObject {
	private MWCell cell;
	
	/**
	 * @param source
	 * @param cell
	 */
	public MWGameCellUpdateEvent(Object source, MWCell cell) {
		super(source);
		this.cell = cell;
	}
	
	
	/** Retourn la case responsable de l'event
	 * @return MWCell
	 */
	public MWCell getCell()
	{
		return this.cell;
	}

}
