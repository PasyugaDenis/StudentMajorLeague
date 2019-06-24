package com.studentmajorleague;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class CompetitionsActivity extends AppCompatActivity {
    String[] competitions = { "ITSprint, Swimming", "CMAS SUPER SPRINT CUP, Swimming", "1st CUP, MMA" };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_competitions);

        ListView view = (ListView) findViewById(R.id.view_competitions);

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_list_item_1, competitions);

        view.setAdapter(adapter);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.main, menu);

        menu.findItem(R.id.nav_users).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(CompetitionsActivity.this, UsersActivity.class);
                startActivity(intent);

                return true;
            }
        });

        menu.findItem(R.id.nav_leagues).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(CompetitionsActivity.this, LeaguesActivity.class);
                startActivity(intent);

                return true;
            }
        });

        menu.findItem(R.id.nav_exit).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                Intent intent = new Intent(CompetitionsActivity.this, MainActivity.class);
                startActivity(intent);

                return true;
            }
        });

        return true;
    }
}
