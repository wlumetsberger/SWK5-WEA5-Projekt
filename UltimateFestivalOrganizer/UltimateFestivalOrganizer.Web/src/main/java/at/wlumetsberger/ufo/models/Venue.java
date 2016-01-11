package at.wlumetsberger.ufo.models;

import lombok.Getter;
import lombok.Setter;

public class Venue extends BaseEntity {
	private static final long serialVersionUID = -1797379116587149078L;
	@Getter @Setter
	private String description;
	@Getter @Setter
	private String shortDescription;
	@Getter @Setter
	private String address;
	@Getter @Setter
	private int longitude;
	@Getter @Setter
	private int latitude;
}
