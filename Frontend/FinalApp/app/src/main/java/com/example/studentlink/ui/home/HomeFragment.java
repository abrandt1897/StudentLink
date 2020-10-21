package com.example.studentlink.ui.home;

import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.R;

import java.util.List;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    private static ListView listview;
    private HomeLogic logic;
    static View.OnClickListener myOnClickListener;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);
        listview = root.findViewById(R.id.listview);
        logic = new HomeLogic(this.getContext());
        notifications = logic.MockGetNotifications();
        listview.setAdapter(new HomeAdapter(this.getContext(), notifications));

        return root;
    }


//    TODO: Delete all this once confident with current solution
//    public void AcceptClickHandler(View v)
//    {
//        //get the row the clicked button is in
//        LinearLayout vwParentRow = (LinearLayout) v.getParent();
//
//        TextView child = (TextView)vwParentRow.getChildAt(0);
//        Button btnChild = (Button)vwParentRow.getChildAt(1);
//        btnChild.setText(child.getText());
//        btnChild.setText("I've been clicked!");
//
//        int c = Color.CYAN;
//        vwParentRow.setBackgroundColor(c);
//        vwParentRow.refreshDrawableState();
//    }

//    private static class MyOnClickListener implements View.OnClickListener {
//
//        private final Context context;
//
//        private MyOnClickListener(Context context) {
//            this.context = context;
//        }
//
//        @Override
//        public void onClick(View v) {
//            removeItem(v);
//        }
//
//        private void removeItem(View v) {
//            int selectedItemPosition = listview.getChildAt(v.);
////                    recyclerView.getChildPosition(v);
//            RecyclerView.ViewHolder viewHolder
//                    = recyclerView.findViewHolderForPosition(selectedItemPosition);
//            TextView textViewName
//                    = (TextView) viewHolder.itemView.findViewById(R.id.textViewName);
//            String selectedName = (String) textViewName.getText();
//            int selectedItemId = -1;
//            for (int i = 0; i < MyData.nameArray.length; i++) {
//                if (selectedName.equals(MyData.nameArray[i])) {
//                    selectedItemId = MyData.id_[i];
//                }
//            }
//            removedItems.add(selectedItemId);
//            data.remove(selectedItemPosition);
//            adapter.notifyItemRemoved(selectedItemPosition);
//        }
//    }
//
//    @Override
//    public boolean onCreateOptionsMenu(Menu menu) {
//        super.onCreateOptionsMenu(menu);
////        getMenuInflater().inflate(R.menu.menu_main, menu);
//        return true;
//    }
//
//    @Override
//    public boolean onOptionsItemSelected(MenuItem item) {
//        super.onOptionsItemSelected(item);
////        if (item.getItemId() == R.id.add_item) {
////            //check if any items to add
////            if (removedItems.size() != 0) {
////                addRemovedItemToList();
////            } else {
////                Toast.makeText(this, "Nothing to add", Toast.LENGTH_SHORT).show();
////            }
////        }
//        return true;
//    }
//
//}


}