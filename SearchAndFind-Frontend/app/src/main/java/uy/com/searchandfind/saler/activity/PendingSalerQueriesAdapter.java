package uy.com.searchandfind.saler.activity;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageButton;
import android.widget.TextView;

import java.util.List;

import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.FieldValidator;
import uy.com.searchandfind.R;
import uy.com.searchandfind.query.QueryDTO;
import uy.com.searchandfind.tender.TenderDTO;

/**
 * Created by strat on 08/11/2017.
 */

public class PendingSalerQueriesAdapter extends BaseAdapter {
    protected Activity activity;
    protected List<QueryDTO> items;
    private Context context;

    public PendingSalerQueriesAdapter (Activity activity, List<QueryDTO> items, Context context) {
        this.activity = activity;
        this.items = items;
        this.context=context;
    }

    @Override
    public int getCount() {
        return items.size();
    }

    public void clear() {
        items.clear();
    }

    public void addAll(List<QueryDTO> queries) {
        for (int i = 0; i < queries.size(); i++) {
            items.add(queries.get(i));
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
            v = inf.inflate(R.layout.pending_queries_list, null);
        }

        final QueryDTO query = items.get(position);
        TextView categoryName = (TextView) v.findViewById(R.id.query_name);
        categoryName.setText(String.valueOf(query.getCategoryName()));


        //if(!FieldValidator.isFieldEmpty(query.getDescription())) {
            TextView  categoryDescription = (TextView) v.findViewById(R.id.query_description);
            categoryDescription.setText(query.getDescription());
        //}

        ImageButton cancelQuery=(ImageButton)v.findViewById(R.id.cancel_query_button);
        cancelQuery.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                List<QueryDTO> queries=(List<QueryDTO>) CacheManager.getInstance().lookup(context.getString(R.string.pending_queries));
                queries.remove(query);
                CacheManager.getInstance().bind(context.getString(R.string.pending_queries),queries);
                CacheManager.notifyObservers(context.getString(R.string.pending_queries));

            }
        });
        return v;
    }

}