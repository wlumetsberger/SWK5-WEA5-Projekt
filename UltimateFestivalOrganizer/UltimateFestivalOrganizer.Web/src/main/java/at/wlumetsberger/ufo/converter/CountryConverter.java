package at.wlumetsberger.ufo.converter;

import java.util.Arrays;
import java.util.Locale;

import javax.enterprise.context.ApplicationScoped;
import javax.faces.model.SelectItem;
import javax.inject.Named;

@ApplicationScoped
@Named("countryConverter")
public class CountryConverter {

	public String toDisplayName(String isoAlpha){
		for(String l : Locale.getISOCountries()){
			Locale loc = new Locale("de",l);
			if(loc.getISO3Country().equals(isoAlpha)){
				return loc.getDisplayCountry();
			}
		}
		return isoAlpha;
		
	}
}
