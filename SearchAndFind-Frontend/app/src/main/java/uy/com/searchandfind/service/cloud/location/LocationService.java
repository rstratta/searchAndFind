package uy.com.searchandfind.service.cloud.location;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Address;
import android.location.Criteria;
import android.location.Geocoder;
import android.location.Location;
import android.location.LocationManager;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.widget.Toast;

import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;

import java.io.IOException;
import java.util.List;
import java.util.Locale;

import uy.com.searchandfind.activity.ShowErrorMessageHandler;

/**
 * Created by strat on 11/10/2017.
 */

public class LocationService {

    public final static int REQUEST_PERMISSION_STATE=1;
    public LocationService() {

    }

    private Criteria getCriteria() {
        Criteria criteria = new Criteria();
        criteria.setAccuracy(Criteria.ACCURACY_COARSE);
        criteria.setPowerRequirement(Criteria.POWER_LOW);
        criteria.setSpeedRequired(false);
        criteria.setAltitudeRequired(false);
        return criteria;
    }



    public LocationDTO getCurrentLocation(Activity activity) throws LocationServiceException {
        LocationManager locationManager = (LocationManager) activity.getApplicationContext().getSystemService(Context.LOCATION_SERVICE);
        Criteria locationCriteria = getCriteria();
        String locationProvider = locationManager.getBestProvider(locationCriteria, true);
        if (locationProvider != null) {
            return getLocationDTO(locationManager, locationProvider,  activity);
        } else {
            throw new LocationServiceException("Debe activar redes o GPS");
        }

    }

    public boolean hasPermissions(Activity activity) {
        if (ActivityCompat.checkSelfPermission(activity, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(activity, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            requestPermissions(activity);
            return false;
        }else{
            return true;
        }
    }

    private void requestPermissions(Activity activity){
        ActivityCompat.requestPermissions(activity,new String[]{Manifest.permission.ACCESS_FINE_LOCATION,Manifest.permission.ACCESS_COARSE_LOCATION},
                REQUEST_PERMISSION_STATE);
    }
    private LocationDTO getLocationDTO(LocationManager locationManager, String locationProvider, Activity activity) {
        Location location = getLocation(locationManager, locationProvider, activity);
        return fillCurrentLocation(location.getLatitude(), location.getLongitude());
    }

    private Location getLocation(LocationManager locationManager, String locationProvider, Activity activity) {
        if (ActivityCompat.checkSelfPermission(activity, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(activity, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
           requestPermissions(activity);
        }
        Location location = locationManager.getLastKnownLocation(locationProvider);
        return location;
    }

    private LocationDTO fillCurrentLocation(double latitude, double longitude) {
        LocationDTO locationDTO=new LocationDTO();
        locationDTO.setLatitude(latitude);
        locationDTO.setLength(longitude);
        return locationDTO;
    }

    public LocationDTO getLocationFromAddress(Context context, String address) throws IOException {
        Geocoder geocoder=new Geocoder(context, Locale.getDefault());
        List<Address> locations=geocoder.getFromLocationName(address,1);
        return fillCurrentLocation(locations.get(0).getLatitude(), locations.get(0).getLongitude());
    }


    public String getAddressFromLocation(Context context, double latitude, double longitude) throws IOException {
        Geocoder geocoder=new Geocoder(context, Locale.getDefault());
        List<Address> locations=geocoder.getFromLocation(latitude,longitude,1);
        return locations.get(0).getAddressLine(0);
    }
}
