package at.wlumetsberger.ufo.beans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.annotation.PostConstruct;
import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.faces.view.ViewScoped;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;
import lombok.Getter;
import lombok.Setter;

@Named(value="showArtistBean")
@ViewScoped
public class ShowArtistBean implements Serializable {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 7239625611905988152L;

	@Inject @Named("QueryService")
	IQueryService queryService;
	
	@Getter @Setter
	private List<Artist> artists;
		
	@PostConstruct
	private void initialize(){
		try{
			artists = queryService.queryArtists();
		}catch(ServiceConnectionException e){
			e.printStackTrace();
			artists = new ArrayList<>();
			FacesMessage message = new FacesMessage();
            message.setSeverity(FacesMessage.SEVERITY_ERROR);
            message.setSummary("Daten können nicht geladen werden, versuchen Sie es nocheimal oder wenden Sie sich an den Administrator");
            message.setDetail("");
            FacesContext.getCurrentInstance().addMessage("", message);
			
		}
	}

}
