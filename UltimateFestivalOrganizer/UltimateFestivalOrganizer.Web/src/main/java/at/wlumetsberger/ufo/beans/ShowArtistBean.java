package at.wlumetsberger.ufo.beans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.stream.Collectors;

import javax.annotation.PostConstruct;
import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.faces.event.ValueChangeEvent;
import javax.faces.model.SelectItem;
import javax.faces.view.ViewScoped;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.models.Catagory;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;
import at.wlumetsberger.ufo.util.FacesHelper;
import lombok.Getter;
import lombok.Setter;

@Named(value="showArtistBean")
@ViewScoped
public class ShowArtistBean implements Serializable {
	
	private static final long serialVersionUID = 7239625611905988152L;

	@Inject @Named("QueryService")
	IQueryService queryService;
	
	@Getter @Setter
	private List<Artist> artists;
	
	@Getter @Setter
	private List<Artist> filteredList;
	
	@Getter @Setter
	private List<SelectItem> catagories;
	
	@Getter @Setter
	private String selectedCatagory;
	
	@Getter @Setter
	private String selectedCountry;
	
	@Getter @Setter
	private List<SelectItem> countries;
	
		
	@PostConstruct
	private void initialize(){
		try{
			artists = queryService.queryArtists();
			this.catagories = new ArrayList<>();
			this.countries = new ArrayList<>();
			this.catagories.add(new SelectItem(null, "Bitte auswählen"));
			this.countries.add(new SelectItem(null, "Bitte auswählen"));
			queryService.queryCatagories().forEach(c -> catagories.add(new SelectItem(c.getId(), c.getName())));
			for(String l : Locale.getISOCountries()){
				Locale loc = new Locale("de",l);
				countries.add(new SelectItem(loc.getISO3Country(), loc.getDisplayCountry()));
			}
			
			this.doFilter();
		}catch(ServiceConnectionException e){
			e.printStackTrace();
			artists = new ArrayList<>();
			FacesHelper.addFacesMessage("Daten können nicht geladen werden, versuchen Sie es nocheimal oder wenden Sie sich an den Administrator", FacesMessage.SEVERITY_ERROR, FacesContext.getCurrentInstance());			
		}	
	}
	
	private void doFilter(){
		filteredList = new ArrayList<>();
		if(selectedCatagory == null && selectedCountry == null ){
			this.filteredList.addAll(this.artists);
			return;
		}
		List<Artist> catagoryFilteredList = new ArrayList<>();
		if(this.selectedCatagory != null && !this.selectedCatagory.isEmpty()){
			int catagoryId = Integer.parseInt(selectedCatagory);
			catagoryFilteredList = artists.stream().filter(a -> a.getCatagory().getId() == catagoryId).collect(Collectors.toList());
		}else{
			catagoryFilteredList.addAll(this.artists);
		}
		if(this.selectedCountry != null && !this.selectedCountry.isEmpty()){
			this.filteredList = catagoryFilteredList.stream().filter(a -> a.getCountry().equals(selectedCountry)).collect(Collectors.toList());
		}else{
			this.filteredList.addAll(catagoryFilteredList);
		}		
	}
	
	public void catagoryChanged(ValueChangeEvent event){
		selectedCatagory = (String)event.getNewValue();
		doFilter();
	}
	
	public void countryChanged(ValueChangeEvent event){
		selectedCountry = (String) event.getNewValue();
		doFilter();
	}
	
	public String getRedirectUrl(String artistId){
		return "showPerformances?faces-redirect=true&artistId="+artistId;
	}

}
