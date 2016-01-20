package at.wlumetsberger.ufo.services.interfaces;

import java.io.Serializable;
import java.time.LocalDate;
import java.util.List;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.models.Catagory;
import at.wlumetsberger.ufo.models.Performance;
import at.wlumetsberger.ufo.models.Venue;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;

public interface IQueryService extends Serializable{

	List<Artist> queryArtists() throws ServiceConnectionException;
	List<Catagory> queryCatagories() throws ServiceConnectionException;
	List<Venue> queryVenues() throws ServiceConnectionException;
	List<Performance> queryPerformancesByDay(LocalDate day) throws ServiceConnectionException;
	List<Performance> queryPerformancesByArtist(int artistId) throws ServiceConnectionException;
	List<Performance> queryPerformancesByVenue(int venueId) throws ServiceConnectionException;
	List<Performance> queryPerformancesByCatagory(int catagoryId) throws ServiceConnectionException;	
}
