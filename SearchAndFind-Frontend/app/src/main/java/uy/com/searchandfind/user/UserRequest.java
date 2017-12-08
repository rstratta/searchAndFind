package uy.com.searchandfind.user;

import uy.com.searchandfind.Request;

/**
 * Created by strat on 30/09/2017.
 */

public class UserRequest extends Request{

    private String LastName ;
    private String Mail ;
    private String Name ;
    private String Password ;
    private String DeviceId ;

    public UserRequest(){}

    public String getLastName() {
        return LastName;
    }

    public void setLastName(String lastName) {
        LastName = lastName;
    }

    public String getMail() {
        return Mail;
    }

    public void setMail(String mail) {
        Mail = mail;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getDeviceId() {
        return DeviceId;
    }

    public void setDeviceId(String deviceId) {
        DeviceId = deviceId;
    }
}
