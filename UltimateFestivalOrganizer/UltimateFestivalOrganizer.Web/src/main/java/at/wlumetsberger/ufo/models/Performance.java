package at.wlumetsberger.ufo.models;

import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.Date;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;

public class Performance extends BaseEntity{

	private static final long serialVersionUID = -2452510428587341397L;
	@Getter @Setter
	@SerializedName("StagingTime")
	private Date stagingTime;
	@Getter @Setter
	@SerializedName("Artist")
	private Artist artist;
	@Getter @Setter
	@SerializedName("Venue")
	private Venue venue;
	@Getter @Setter
	@SerializedName("Canceld")
	private boolean canceld;
	
	public String getFormattedStagingTime(){
		SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm");
		return df.format(stagingTime);
				
	}
}
