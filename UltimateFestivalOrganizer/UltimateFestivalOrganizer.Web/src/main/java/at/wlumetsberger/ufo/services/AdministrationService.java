package at.wlumetsberger.ufo.services;

import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import at.wlumetsberger.ufo.models.dataTransfer.PosponePerformance;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IAdministrationService;

public class AdministrationService implements IAdministrationService {

	
	private static final long serialVersionUID = -1743171194297061543L;
	
	private RestClient restClient;
	
	public AdministrationService(RestClient c) {
		this.restClient=c;
		 
	}
	
	@Override
	public boolean checkPostPonePerformance(Date date, int performanceId, int venueId,String userId){
		PosponePerformance p = new PosponePerformance(performanceId, date, venueId);
		try {
			Boolean result = restClient.<Boolean>sendPostRequest("performance/postponeCheck", userId, p, Boolean.class);
			return result;
		} catch (ServiceConnectionException e) {
			e.printStackTrace();
			return false;
		}	
	}
	
	@Override
	public boolean postPonePerformance(Date date, int performanceId, int venueId,  String userId) {
		PosponePerformance p = new PosponePerformance(performanceId, date, venueId);
		try {
			Boolean result = restClient.<Boolean>sendPostRequest("performance/postpone", userId, p, Boolean.class);
			return result;
		} catch (ServiceConnectionException e) {
			e.printStackTrace();
			return false;
		}	
		
	
	}
	@Override
	public boolean canclePerformance(int performanceId, String userId) throws ServiceConnectionException {
		PosponePerformance p = new PosponePerformance(performanceId, new Date(),0);
		try{
			Boolean result = restClient.<Boolean>sendPostRequest("performance/cancel", userId, p, Boolean.class);
			return result;
		} catch (ServiceConnectionException e) {
			e.printStackTrace();
			return false;
		}	
	}

}
