package uy.com.searchandfind.query.activity;

import android.app.Activity;
import android.content.Context;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.List;

import uy.com.searchandfind.R;
import uy.com.searchandfind.tender.TenderDTO;

/**
 * Created by strat on 06/11/2017.
 */

public class QueryResponseItemAdapter extends BaseAdapter {
    protected Activity activity;
    protected List<TenderDTO> items;

    public QueryResponseItemAdapter (Activity activity, List<TenderDTO> items) {
        this.activity = activity;
        this.items = items;
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
            v = inf.inflate(R.layout.tender_list, null);
        }

        TenderDTO tender = items.get(position);
        TextView tenderAmount = (TextView) v.findViewById(R.id.tender_amount);
        tenderAmount.setText("$ "+String.valueOf(tender.getTenderAmount()));

        TextView tenderDescription = (TextView) v.findViewById(R.id.tender_description);
        tenderDescription.setText(tender.getDescription());

        TextView rate = (TextView) v.findViewById(R.id.tender_rating);
        rate.setText( String.valueOf(tender.getSalerDTO().getAverageReview()).substring(0,3) + " (" + tender.getSalerDTO().getNumberOfReview() + ")");
        return v;
    }

}