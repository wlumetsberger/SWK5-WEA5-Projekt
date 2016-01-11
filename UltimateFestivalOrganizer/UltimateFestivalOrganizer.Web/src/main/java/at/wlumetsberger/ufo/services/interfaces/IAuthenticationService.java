package at.wlumetsberger.ufo.services.interfaces;

import java.io.Serializable;

import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;

public interface IAuthenticationService extends Serializable{

	boolean authenticateUser(String userId) throws ServiceConnectionException;
}
