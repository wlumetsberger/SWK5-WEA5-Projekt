package at.wlumetsberger.ufo.services.exceptions;

import lombok.Getter;

public class ServiceConnectionException extends Exception{
	
	@Getter
	private int statuscode;
	
	public ServiceConnectionException(int statuscode, String message) {
		super(message);
		this.statuscode = statuscode;
	}

}
