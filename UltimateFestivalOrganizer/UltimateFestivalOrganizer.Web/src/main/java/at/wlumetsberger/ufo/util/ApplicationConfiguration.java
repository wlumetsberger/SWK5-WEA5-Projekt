package at.wlumetsberger.ufo.util;

import lombok.Getter;
import lombok.Setter;

public class ApplicationConfiguration {

	private static ApplicationConfiguration instance;
	
	@Getter @Setter
	private String restServiceUrl;
	
	public static ApplicationConfiguration getInstance(){
		if(instance == null){
			instance = new ApplicationConfiguration();
		}
		return instance;
	}
	
	public void init(String restServiceUrl){
		this.restServiceUrl = restServiceUrl;
	}
}
