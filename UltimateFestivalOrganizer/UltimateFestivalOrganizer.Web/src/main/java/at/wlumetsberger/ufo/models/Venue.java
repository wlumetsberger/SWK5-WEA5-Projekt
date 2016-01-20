package at.wlumetsberger.ufo.models;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;

public class Venue extends BaseEntity {
	private static final long serialVersionUID = -1797379116587149078L;
	@Getter @Setter
	@SerializedName("Description")
	private String description;
	@Getter @Setter
	@SerializedName("ShortDescription")
	private String shortDescription;
	@Getter @Setter
	@SerializedName("Address")
	private String address;
	@Getter @Setter
	@SerializedName("Longitude")
	private int longitude;
	@Getter @Setter
	@SerializedName("Latitude")
	private int latitude;
}
