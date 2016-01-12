package at.wlumetsberger.ufo.beans;

import java.io.Serializable;

import javax.annotation.PostConstruct;
import javax.faces.view.ViewScoped;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.services.interfaces.IQueryService;

@Named(value="ShowPerformancesBean")
@ViewScoped
public class ShowPerformancesBean implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = -5285467173103720880L;
	@Inject @Named("QueryService") 
	private IQueryService queryService;
	
	
	public ShowPerformancesBean(){
	}
	
	@PostConstruct
	private void initialize(){
		
	}
}
