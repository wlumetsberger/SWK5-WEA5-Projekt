package at.wlumetsberger.ufo.models;

import java.io.Serializable;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;

public abstract class BaseEntity implements Serializable {

	private static final long serialVersionUID = -6342487781666150571L;
	
	@Getter @Setter
	@SerializedName("Id")
	private Integer id;
}
