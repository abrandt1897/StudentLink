package com.example.studentlink.ui.profile;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.studentlink.R;

import java.util.ArrayList;

public class RecycleAdapter extends RecyclerView.Adapter<RecycleAdapter.RecycleViewHolder> {

    private ArrayList<classObject> objects;

    public static  class RecycleViewHolder extends RecyclerView.ViewHolder {
        public TextView classText;

        public RecycleViewHolder(@NonNull View itemView) {
            super(itemView);
            classText = itemView.findViewById(R.id.classText);
        }
    }

    public RecycleAdapter(ArrayList<classObject> classObjects) {
        objects = classObjects;
    }

    @NonNull
    @Override
    public RecycleViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.profile_class_item, parent, false);
        RecycleViewHolder rvh = new RecycleViewHolder(v);
        return rvh;
    }

    @Override
    public void onBindViewHolder(@NonNull RecycleViewHolder holder, int position) {
        classObject currentObject = objects.get(position);
        holder.classText.setText(currentObject.getClassText());
    }

    @Override
    public int getItemCount() {
        return objects.size();
    }
}
