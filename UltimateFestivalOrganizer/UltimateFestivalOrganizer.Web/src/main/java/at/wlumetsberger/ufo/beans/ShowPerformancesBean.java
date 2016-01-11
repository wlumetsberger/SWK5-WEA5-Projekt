package at.wlumetsberger.ufo.beans;

import java.io.Serializable;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.view.ViewScoped;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.services.QueryService;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;

@Named(value="ShowPerformancesBean")
@ViewScoped
public class ShowPerformancesBean implements Serializable {

	@Inject @Named("QueryService") 
	private IQueryService queryService;
	
	
	public ShowPerformancesBean(){
	}
	
	@PostConstruct
	private void initialize(){
		
	}
}
