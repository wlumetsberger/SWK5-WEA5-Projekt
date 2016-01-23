package at.wlumetsberger.ufo.converter;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Named;

@ApplicationScoped
@Named("disabledConverter")
public class DisabledConverter {

	
	public String convert(boolean disabled){
		System.out.println("disabled: "+disabled);
		return disabled?"disabled":"";
	}
}
