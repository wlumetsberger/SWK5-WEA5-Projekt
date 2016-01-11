package at.wlumetsberger.ufo.models;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;

public class Catagory extends BaseEntity{
	private static final long serialVersionUID = 7787903120512456442L;
	@Getter @Setter
	@SerializedName("Name")
	private String name;
	@Getter @Setter
	@SerializedName("Description")
	private String description;
}
