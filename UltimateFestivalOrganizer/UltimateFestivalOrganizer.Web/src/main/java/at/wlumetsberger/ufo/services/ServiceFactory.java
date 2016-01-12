package at.wlumetsberger.ufo.services;

import javax.enterprise.context.ApplicationScoped;
import javax.enterprise.inject.Produces;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.services.interfaces.IAuthenticationService;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;

@ApplicationScoped
public class ServiceFactory {

	@Inject
	private RestClient client;
	
	@Produces @Named("QueryService")
	public  IQueryService getQueryService(){
		return new QueryService(this.client);
	}
	
	@Produces @Named("AuthenticationService")
	public  IAuthenticationService getAuthenticationService(){
		return new AuthenticationService(this.client);
	}	
	
}
