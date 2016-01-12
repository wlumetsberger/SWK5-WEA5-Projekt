package at.wlumetsberger.ufo.models;

import java.time.LocalTime;

import lombok.Getter;
import lombok.Setter;

public class Performance extends BaseEntity{

	private static final long serialVersionUID = -2452510428587341397L;
	@Getter @Setter
	private LocalTime stagingTime;
	@Getter @Setter
	private Artist artist;
	@Getter @Setter
	private Venue venue;
}
