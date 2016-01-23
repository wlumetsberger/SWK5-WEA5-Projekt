package at.wlumetsberger.ufo.services.exceptions;

import lombok.AccessLevel;
import lombok.Getter;
import lombok.Setter;

public class ServiceConnectionException extends Exception{
	
	private static final long serialVersionUID = 1669245736975227565L;
	@Getter @Setter(AccessLevel.PRIVATE)
	private int statuscode;
	
	public ServiceConnectionException(int statuscode, String message) {
		super(message);
		this.statuscode = statuscode;
	}

}
