package at.wlumetsberger.ufo.beans;

import java.io.Serializable;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
import java.util.List;

import javax.annotation.PostConstruct;
import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.faces.event.AjaxBehaviorEvent;
import javax.faces.event.ValueChangeEvent;
import javax.faces.model.SelectItem;
import javax.faces.view.ViewScoped;
import javax.inject.Inject;
import javax.inject.Named;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.models.Catagory;
import at.wlumetsberger.ufo.models.Performance;
import at.wlumetsberger.ufo.models.Venue;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;
import at.wlumetsberger.ufo.services.interfaces.IAdministrationService;
import at.wlumetsberger.ufo.services.interfaces.IQueryService;
import at.wlumetsberger.ufo.util.FacesHelper;
import lombok.Getter;
import lombok.Setter;

@Named(value = "showPerformancesBean")
@ViewScoped
public class ShowPerformancesBean implements Serializable {

	private static final long serialVersionUID = -5285467173103720880L;

	@Inject
	@Named("QueryService")
	private IQueryService queryService;

	@Inject
	@Named("AdministrationService")
	private IAdministrationService administrationService;

	@Inject
	UserBean userBean;

	private DateTimeFormatter dateFromatter;

	@Getter
	@Setter
	private String performanceDescription;
	@Getter
	@Setter
	private List<SelectItem> catagories;
	@Getter
	@Setter
	private List<SelectItem> artists;
	@Getter
	@Setter
	private List<SelectItem> venues;
	@Getter
	@Setter
	private List<Performance> performances;
	@Getter
	@Setter
	private List<Performance> filteredPerformances;
	@Getter
	@Setter
	private String catagory;
	@Getter
	@Setter
	private String venue;
	@Getter
	@Setter
	private String artist;
	@Getter
	@Setter
	private String stagingDate;
	@Getter
	@Setter
	private Performance currentPerformance;
	@Getter
	@Setter
	private boolean showPostpone = false;
	@Getter
	@Setter
	private String postPoneDate;
	@Getter
	@Setter
	private String postPoneVenue;

	public ShowPerformancesBean() {

	}

	@PostConstruct
	private void initialize() {
		try {
			String artistId = FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap()
					.get("artistId");
			if (artistId != null) {
				this.artist = artistId;
			}
		} catch (Exception e) {
			// do search without request parameter
		}
		this.dateFromatter = DateTimeFormatter.ofPattern("yyyy-MM-dd");
		try {
			List<Artist> artists = queryService.queryArtists();
			List<Catagory> catagories = queryService.queryCatagories();
			List<Venue> venues = queryService.queryVenues();
			this.filteredPerformances = new ArrayList<>();
			this.artists = new ArrayList<>();
			this.catagories = new ArrayList<>();
			this.venues = new ArrayList<>();
			if (this.artist == null) {
				this.stagingDate = dateFromatter.format(LocalDate.now());
			}
			this.artists.add(new SelectItem(null, "Bitte auswählen"));
			this.catagories.add(new SelectItem(null, "Bitte auswählen"));
			this.venues.add(new SelectItem(null, "Bitte auswählen"));
			for (Venue venue : venues) {
				this.venues.add(new SelectItem(venue.getId(), venue.getDescription()));
			}
			for (Catagory catagory : catagories) {
				this.catagories.add(new SelectItem(catagory.getId(), catagory.getName()));
			}
			for (Artist artist : artists) {
				this.artists.add(new SelectItem(artist.getId(), artist.getName()));
			}
			this.doSearch();
		} catch (ServiceConnectionException e) {
			e.printStackTrace();
			FacesHelper.addFacesMessage("Daten können nicht geladen werden, versuchen Sie es nocheimal oder wenden Sie sich an den Administrator", FacesMessage.SEVERITY_ERROR, FacesContext.getCurrentInstance());			

		}
	}

	public void venueChanged(ValueChangeEvent event) {
		if (event.getNewValue() != null) {
			this.venue = (String) event.getNewValue();
			this.doSearch();
		} else {
			this.venue = null;
			this.doSearch();
		}
	}

	public void catagoryChanged(ValueChangeEvent event) {
		if (event.getNewValue() != null) {
			this.catagory = (String) event.getNewValue();
			this.doSearch();
		} else {
			this.catagory = null;
			this.doSearch();
		}
	}

	public void artistChanged(ValueChangeEvent event) {
		if (event.getNewValue() != null) {
			this.artist = (String) event.getNewValue();
			this.doSearch();
		} else {
			this.artist = null;
			this.doSearch();
		}
	}

	public void postPoneVenueChanged(ValueChangeEvent event) {
		if (event.getNewValue() != null) {
			this.postPoneVenue = (String) event.getNewValue();
		}
	}

	public void openDetails(Performance current) {
		this.currentPerformance = current;
		this.showPostpone = false;
		System.out.println("long: " + currentPerformance.getVenue().getLongitude() + " lat: "
				+ currentPerformance.getVenue().getLatitude());
	}

	public void openPostpone() {
		this.showPostpone = true;
		this.postPoneVenue = String.valueOf(this.currentPerformance.getVenue().getId());
		System.out.println("show postpone+ " + this.showPostpone);
	}

	public void cancelPerformance() {
		System.out.println("cancelPerformance");
		try {
			if (this.administrationService.canclePerformance(this.currentPerformance.getId(), userBean.getUserId())) {
				FacesHelper.addFacesMessage("Veranstaltung wurde abgesagt", FacesMessage.SEVERITY_INFO,
						FacesContext.getCurrentInstance());
				this.doSearch();
				return;
			}
		} catch (ServiceConnectionException e) {
			e.printStackTrace();
		}
		FacesHelper.addFacesMessage("Ein Fehler ist aufgetreten: Die Veranstaltung konnte nicht abgesagt werden",
				FacesMessage.SEVERITY_ERROR, FacesContext.getCurrentInstance());

	}

	public void cancelOpenDialog() {
		this.showPostpone = false;
		this.currentPerformance = null;
	}

	public void doPostpone() {
		try {

			DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH");
			Date d = df.parse(this.postPoneDate);
			int venueId = Integer.parseInt(this.postPoneVenue);
			if (!administrationService.checkPostPonePerformance(d, this.currentPerformance.getId(), venueId,
					userBean.getUserId())) {
				FacesHelper.addFacesMessage(
						"Veranstaltung konnte nicht auf den gewünschten Termin verschoben werden. Das Datum kollidiert mit einem anderen Termin des Künstlers",
						FacesMessage.SEVERITY_ERROR, FacesContext.getCurrentInstance());
				this.showPostpone = false;
				return;
			}
			if (administrationService.postPonePerformance(d, this.currentPerformance.getId(), venueId,
					userBean.getUserId())) {
				FacesHelper.addFacesMessage("Veranstaltung wurde erfolgreich verschoben", FacesMessage.SEVERITY_INFO,
						FacesContext.getCurrentInstance());
				this.stagingDate = new SimpleDateFormat("yyyy-MM-dd").format(d);
				this.artist = String.valueOf(currentPerformance.getArtist().getId());
				this.catagory = String.valueOf(currentPerformance.getArtist().getCatagory().getId());
				this.venue = String.valueOf(venueId);
				this.showPostpone = false;
				this.doSearch();
				return;
			}
		} catch (ParseException e) {
			e.printStackTrace();
		}
		FacesHelper.addFacesMessage("Veranstaltung konnte nicht verschoben werden", FacesMessage.SEVERITY_ERROR,
				FacesContext.getCurrentInstance());
	}

	public void doSearch() {
		try {
			if (stagingDate == null && venue == null && artist == null && catagory == null) {
				FacesHelper.addFacesMessage("Es muss mindestens eine Einschränkung getroffen werden",
						FacesMessage.SEVERITY_WARN, FacesContext.getCurrentInstance());
				return;
			}
			if (stagingDate != null && !stagingDate.isEmpty()) {
				LocalDate d = LocalDate.parse(stagingDate, dateFromatter);
				performances = queryService.queryPerformancesByDay(d);
				System.out.println("filter by date");
			} else if (venue != null && !venue.isEmpty()) {
				performances = queryService.queryPerformancesByVenue(Integer.parseInt(venue));
				System.out.println("filter by venue");
			} else if (artist != null && !artist.isEmpty()) {
				performances = queryService.queryPerformancesByArtist(Integer.parseInt(artist));
				System.out.println("filter by artist");
			} else if (catagory != null && !catagory.isEmpty()) {
				performances = queryService.queryPerformancesByCatagory(Integer.parseInt(catagory));
				System.out.println("filter by catagory");
			}
			this.doFilter();

		} catch (Exception e) {
			e.printStackTrace();
			FacesHelper.addFacesMessage("Daten können nicht geladen werden, versuchen Sie es nocheimal oder wenden Sie sich an den Administrator", FacesMessage.SEVERITY_ERROR, FacesContext.getCurrentInstance());			

		}
	}

	public String getTimelineClass(boolean even) {
		return even ? "" : "timeline-inverted";
	}

	public void doFilter() {
		this.filteredPerformances.clear();
		this.filteredPerformances.addAll(this.performances);
		this.filterArtists();
		this.filterCatagories();
		this.filterVenues();
		Collections.sort(this.filteredPerformances, new Comparator<Performance>() {
			@Override
			public int compare(Performance o1, Performance o2) {
				return o1.getStagingTime().compareTo(o2.getStagingTime());
			}
		});
	}

	public void filterVenues() {
		if (this.venue != null && !this.venue.isEmpty() && !this.venue.equals("0")) {
			int venueId = Integer.parseInt(venue);
			for (Performance p : performances) {
				if (!p.getVenue().getId().equals(venueId)) {
					this.filteredPerformances.remove(p);
				}
			}
		}

	}

	public void filterArtists() {
		if (this.artist != null && !this.artist.isEmpty() && !this.artist.equals("0")) {
			int artistId = Integer.parseInt(artist);
			for (Performance p : performances) {
				if (!p.getArtist().getId().equals(artistId)) {
					this.filteredPerformances.remove(p);
				}
			}
		}

	}

	public void filterCatagories() {
		if (this.catagory != null && !this.catagory.isEmpty() && !this.catagory.equals("0")) {
			int catagoryId = Integer.parseInt(catagory);
			for (Performance p : performances) {
				if (!p.getArtist().getCatagory().getId().equals(catagoryId)) {
					this.filteredPerformances.remove(p);
				}
			}
		}
	}
}
