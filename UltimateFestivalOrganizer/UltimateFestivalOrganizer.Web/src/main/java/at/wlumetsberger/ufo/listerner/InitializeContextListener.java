package at.wlumetsberger.ufo.listerner;

import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import at.wlumetsberger.ufo.util.ApplicationConfiguration;

public class InitializeContextListener implements ServletContextListener {

	

	@Override
	public void contextInitialized(ServletContextEvent arg0) {
		ServletContext servletContext = arg0.getServletContext();
		String serviceUrl = servletContext.getInitParameter("serviceUrl");
		ApplicationConfiguration.getInstance().init(serviceUrl);
		
	}

	@Override
	public void contextDestroyed(ServletContextEvent sce) {
		
	}


	
}

