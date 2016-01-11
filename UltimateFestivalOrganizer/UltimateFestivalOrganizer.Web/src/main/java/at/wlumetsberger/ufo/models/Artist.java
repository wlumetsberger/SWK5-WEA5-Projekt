package at.wlumetsberger.ufo.models;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;

public class Artist extends BaseEntity {
	private static final long serialVersionUID = 4950996861427703886L;
	@Getter @Setter
	@SerializedName("Email")
	private String email;
	@Getter @Setter
	@SerializedName("Name")
	private String name;
	@Getter @Setter
	@SerializedName("Link")
	private String link;
	@Getter @Setter
	@SerializedName("Picture")
	private String picture;
	@Getter @Setter
	@SerializedName("Catagory")
	private Catagory catagory;
	@Getter @Setter
	@SerializedName("Country")
	private String country;
}
