package mineswreeper.exception;



@SuppressWarnings("serial")
public class MWTooMuchMineAtInitializationException extends RuntimeException {
	
	public MWTooMuchMineAtInitializationException(String message) 
	{
		super(message);
	}
}
