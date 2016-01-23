package at.wlumetsberger.ufo.converter;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Named;

@ApplicationScoped
@Named("koordinateConverter")
public class KoordinateConverter {

	
	public double toLongitude(int longitude){
		return longitude / 1000000;
	}
	
	public double toLatitude(int latitude){
		return latitude / 1000000;
	}
}
