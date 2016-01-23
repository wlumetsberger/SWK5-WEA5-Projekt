package at.wlumetsberger.ufo.services.interfaces;

import java.io.Serializable;
import java.time.LocalDateTime;
import java.util.Date;

import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;

public interface IAdministrationService  extends Serializable{
	
	boolean checkPostPonePerformance(Date date, int performanceId, int venueId,String userId );
	boolean postPonePerformance(Date date, int performanceId, int venueId, String userId);
	boolean canclePerformance(int performanceId, String userId)throws ServiceConnectionException;
	

}
