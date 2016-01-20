package at.wlumetsberger.ufo.beans;

import java.io.Serializable;
import java.util.Base64;

import javax.annotation.PostConstruct;
import javax.enterprise.context.SessionScoped;
import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IAuthenticationService;
import lombok.Getter;
import lombok.Setter;


@Named(value="userBean")
@SessionScoped
public class UserBean implements Serializable {

	private static final long serialVersionUID = -1106655528866778281L;

	@Getter @Setter
	private boolean loggedIn;
	
	@Getter @Setter
	private String userName;
	
	@Getter @Setter
	private String password;
	
	@Getter @Setter
	private String userId;
	
	@Getter @Setter
	private boolean showLogginForm;
	
	@Inject @Named("AuthenticationService")
	IAuthenticationService authenticationService;
	
	public UserBean(){
		
	}
	
	@PostConstruct
	public void initialize(){
		
		this.loggedIn = false;
		this.userName = null;
		this.password = null;
	}
	
	public void showLogin(){
		this.setShowLogginForm(true);
	}
	
	public void dismissLogin(){
		this.setShowLogginForm(false);
	}
	
	public void doLogin(){
		System.out.println("doLogin");
		String userPassword = this.userName + ":"+ this.password;
		String encoding = new String(Base64.getEncoder().encode(userPassword.getBytes()));
		try{
		if(authenticationService.authenticateUser(encoding)){
			this.loggedIn = true;			
			FacesMessage message = new FacesMessage();
            message.setSeverity(FacesMessage.SEVERITY_INFO);
            message.setSummary("Herzlich Willkommen "+ userName);
            FacesContext.getCurrentInstance().addMessage("", message);
            this.userId = encoding;
		}else{
			this.loggedIn = false;
			FacesMessage message = new FacesMessage();
            message.setSeverity(FacesMessage.SEVERITY_ERROR);
            message.setSummary("Loggin Fehlgeschlagen.");
            message.setDetail("Benutzername oder Passwort ist falsch.");
            FacesContext.getCurrentInstance().addMessage("", message);
            this.userId = null;
		}}catch(ServiceConnectionException e){
			e.printStackTrace();
			this.loggedIn = false;
			FacesMessage message = new FacesMessage();
            message.setSeverity(FacesMessage.SEVERITY_ERROR);
            message.setSummary("Loggin Fehlgeschlagen.");
            message.setDetail("Benutzername oder Passwort ist falsch.");
            FacesContext.getCurrentInstance().addMessage("", message);
            this.userId = null;
            this.userName= null;
            this.password = null;
		}
	}
	
	public void doLogout(){
		this.userName = null;
		this.password = null;
		this.loggedIn = false;
	}
}
