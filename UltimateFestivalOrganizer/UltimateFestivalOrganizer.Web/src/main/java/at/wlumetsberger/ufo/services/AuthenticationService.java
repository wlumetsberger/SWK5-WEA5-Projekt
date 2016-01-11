package at.wlumetsberger.ufo.services;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IAuthenticationService;

public class AuthenticationService implements IAuthenticationService {

	private RestClient restClient;

	public AuthenticationService(RestClient client) {
		this.restClient = client;
	}

	
	public boolean authenticateUser(String userId) throws ServiceConnectionException{
		Map<String,String> params = new HashMap<>();
		params.put("email", "test");
		String result = restClient.<String>fetchJsonData("user",params,userId,String.class);
		return "test".equals(result);
		
	}
}
