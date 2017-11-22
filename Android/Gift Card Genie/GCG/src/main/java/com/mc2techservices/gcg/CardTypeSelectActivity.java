package com.mc2techservices.gcg;


import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

public class CardTypeSelectActivity extends Activity {
	String pGCardType;
	String pGURL;
	String pGPassedCategory;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_card_type_select);
		pGPassedCategory = getIntent().getStringExtra("Category");
		Populate();
	}
	@Override
	public void onResume () {
		super.onResume();
		if (AppSpecific.gloFinishIt==true)
		{
			finish();
		}
	}

	public void Populate() {
		LinearLayout mainLL = (LinearLayout) findViewById(R.id.llMainCardTypes);
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String pWhereClause="";
		if (pGPassedCategory.length()>1)
		{
			pWhereClause = " CICategory = '" + pGPassedCategory + "'";
		}
		String pSQLStatement="SELECT * FROM tblCardInfo";
		if (pWhereClause.length()>1)
		{
			pSQLStatement = pSQLStatement + " WHERE" + pWhereClause;
		}
		String AllSavedData=myDb.getAllRowsAsString(pSQLStatement);
		LinearLayout temprow = BuildNewEntryRow();
		mainLL.addView(temprow);
		if (AllSavedData.equals("l#d"))
		{
			GeneralFunctions01.Oth.Alert(this,"Oops, the card info table is empty.  Please re-install.");
			return;
		}
		String[] lines=AllSavedData.split("l#d");
		String templine="";
		String rowBGColor="";
		for (int i = 0; i < lines.length; i++) {
			if (lines[0].equals("")) {
				break;
			}
			String[] vals=lines[i].split("p#d");
			LinearLayout row = BuildTableRow(vals[1],vals[2]);
			mainLL.addView(row);
		}
	}
	private LinearLayout BuildNewEntryRow() {
		LinearLayout row = (LinearLayout)LayoutInflater.from(this).inflate(R.layout.cell_new_entry, null);
		((TextView)row.findViewById(R.id.txtSubHeader)).setText("Unlisted - Manual Entry");
	    row.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				SwitchScreens("-1","");
			}
		});
		return row;
	}
	private LinearLayout BuildTableRow(final String pType, final String pCategory) {
		LinearLayout row = (LinearLayout)LayoutInflater.from(this).inflate(R.layout.cell_select_a_card, null);
		((TextView)row.findViewById(R.id.txtCellCardType)).setText(pType);
		((TextView)row.findViewById(R.id.txtCellCategory)).setText(pCategory);
	    row.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				TextView txtCellCardType= (TextView)v.findViewById(R.id.txtCellCardType);
				String pCellCardType=GeneralFunctions01.Text.GetVal(txtCellCardType);
				SwitchScreens("0",pCellCardType);
			}
		});
		return row;
	}
	private void SwitchScreens(String pID, String pCardType)
	{
		Intent intent = new Intent(this, AddModCardActivity.class);
		intent.putExtra("CardID", pID);
		intent.putExtra("CardType", pCardType);
		startActivity(intent);
		//finish();
	}
}
