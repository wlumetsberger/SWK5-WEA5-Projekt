package at.wlumetsberger.ufo.util;

import javax.faces.application.FacesMessage;
import javax.faces.application.FacesMessage.Severity;
import javax.faces.context.FacesContext;

public class FacesHelper {
	
	public static void addFacesMessage(String message, Severity severity ,FacesContext context){
		FacesMessage m = new FacesMessage();
        m.setSeverity(severity);
        m.setSummary(message);
        context.getCurrentInstance().addMessage("", m);
	}

}
