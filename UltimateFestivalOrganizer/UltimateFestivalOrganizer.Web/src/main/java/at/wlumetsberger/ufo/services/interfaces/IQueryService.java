package at.wlumetsberger.ufo.services.interfaces;

import java.io.Serializable;
import java.util.List;

import at.wlumetsberger.ufo.models.Artist;
import at.wlumetsberger.ufo.services.exceptions.ServiceConnectionException;

public interface IQueryService extends Serializable{

	List<Artist> queryArtists() throws ServiceConnectionException;
}
