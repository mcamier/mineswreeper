package mineswreeper.exception;



@SuppressWarnings("serial")
public class MWTooFewMineAtInitializationException extends RuntimeException {
	
	public MWTooFewMineAtInitializationException(String message) 
	{
		super(message);
	}
}
