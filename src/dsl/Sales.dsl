module Sales {
	aggregate Order {
		string Description;
		money Cost;
		Timestamp CreatedAt { sequence; }
	}

	value RevenjCommandExampleArguments {
		string OrderURI1;
		string OrderURI2;
	}
}
