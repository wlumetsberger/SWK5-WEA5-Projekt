package at.wlumetsberger.ufo.models.dataTransfer;

import java.time.LocalDateTime;
import java.util.Date;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;

public class PosponePerformance implements IBaseDataTransferObject {
	
	@Getter
	@Setter
	@SerializedName("Id")
	private int id;
	
	@Getter
	@Setter
	@SerializedName("StagingTime")
	private Date stagingTime;
	
	
	@Getter
	@Setter
	@SerializedName("VenueId")
	private int venueId;
	
	public PosponePerformance(int id, Date stagingTime, int venueId) {
		this.id = id;
		this.stagingTime = stagingTime;
		this.venueId = venueId;
	}
	
	

}
