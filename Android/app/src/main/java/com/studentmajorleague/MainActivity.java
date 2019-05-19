package com.studentmajorleague;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void login(View view) {
        EditText editTextLogin = findViewById(R.id.user_email);
        EditText editTextPassword = findViewById(R.id.user_password);

        String email = editTextLogin.getText().toString();
        String password = editTextPassword.getText().toString();

        if(email.equals("denys.payuga.99@gmail.com") && password.equals("qwerty")){
            Intent intent = new Intent(this, UsersActivity.class);
            startActivity(intent);
        }
    }

    public void register(View view) {
        Intent intent = new Intent(this, RegisterActivity.class);
        startActivity(intent);
    }
}
