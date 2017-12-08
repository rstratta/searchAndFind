package uy.com.searchandfind.client.tenders;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v4.content.ContextCompat;
import android.util.Base64;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;


import java.util.List;

import uy.com.searchandfind.R;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.tender.TenderDTO;

/**
 * Created by Luis on 27/10/2017.
 */

public class AdapterTender extends BaseAdapter {
    protected Activity activity;
    protected List<TenderDTO> items;
    private boolean isSaler;

    public AdapterTender (Activity activity, List<TenderDTO> items) {
        this.activity = activity;
        this.items = items;
        this.isSaler = getProfile();
    }

    @Override
    public int getCount() {
        return items.size();
    }

    public void clear() {
        items.clear();
    }

    public void addAll(List<TenderDTO> tenders) {
        for (int i = 0; i < tenders.size(); i++) {
            items.add(tenders.get(i));
        }
    }

    private boolean getProfile(){
        String profile = SharedPreferenceHandler.getValue(activity, activity.getString(R.string.current_profile));
        return (profile.equals(activity.getString(R.string.saler_profile)));
    }

    @Override
    public Object getItem(int item) {
        return items.get(item);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        View v = convertView;

        if (convertView == null) {
            LayoutInflater inf = (LayoutInflater) activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            v = inf.inflate(R.layout.activity_history_list, null);
        }

        TenderDTO tender = items.get(position);

        //TextView tenderDate = (TextView) v.findViewById(R.id.tenderDate);
        //tenderDate.setText(tender.getTenderDate().toString());

        TextView tenderAmmount = (TextView) v.findViewById(R.id.tenderAmmount);
        tenderAmmount.setText(String.valueOf(tender.getTenderAmount()));

        TextView tenderDescription = (TextView) v.findViewById(R.id.tenderDescription);
        tenderDescription.setText(tender.getDescription());

        ImageView image = (ImageView) v.findViewById(R.id.tenderImage);
        if (tender.getImages().isEmpty()){
            Drawable imageSYF =  ContextCompat.getDrawable(activity, R.drawable.syflogo);
            image.setImageDrawable(imageSYF);
        }
        else{
            byte[] decodedString = Base64.decode(tender.getImages().get(0), Base64.DEFAULT);
            Bitmap imageDecodedByte = BitmapFactory.decodeByteArray(decodedString, 0, decodedString.length);
            image.setImageBitmap(imageDecodedByte);
        }

        TextView myRate = (TextView) v.findViewById(R.id.myRate);
        if ((tender.getPointsFromSaler() != 0 && isSaler) || (tender.getPointsFromClient()!=0 && !isSaler) ) {
            TextView pending = (TextView) v.findViewById(R.id.pending);
            pending.setText("");
        }
        ImageView star = (ImageView) v.findViewById(R.id.star);
        if ((tender.getPointsFromClient() == 0 && isSaler) || (tender.getPointsFromSaler() == 0 && !isSaler)) {
            star.setVisibility(View.INVISIBLE);
            myRate.setText("");
        }else{
            star.setVisibility(View.VISIBLE);
            if (isSaler)
                myRate.setText(String.valueOf(tender.getPointsFromClient()).substring(0,1));
            else
                myRate.setText(String.valueOf(tender.getPointsFromSaler()).substring(0,1));
        }
        return v;
    }

}
