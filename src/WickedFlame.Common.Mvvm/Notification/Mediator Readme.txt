The Mediator was downloaded with the ServiceLocator Framework from:
http://marlongrech.wordpress.com/2009/09/02/service-locator-in-mvvm/

Mediator description:
http://marlongrech.wordpress.com/2009/04/16/mediator-v2-for-mvvm-wpf-and-silverlight-applications/


code:


Im BaseViewModel:
	public BaseViewModel()
	{
		...
		// register this instance to the mediator pattern
		Mediator.Register(this);
	}

	#region Mediator for ViewModels

	static readonly Mediator mediator = new Mediator();

	public Mediator Mediator
	{
			get { return mediator; }
	}

	#endregion


Im ViewModel das aktualisiert werden muss wenn ein neuer Kurs erfasst wird kann mit dem MediatorMessageSink Attribut oder mit Lambda delegates gearbeitet werden.
MediatorMessageSink Attribut:
	/// <summary>
	/// Subscribe to KursAktualisieren
	/// </summary>
	/// <param name="message">The message to be passed</param>
	[MediatorMessageSink("KursAktualisieren", ParameterType = typeof(Kurs))]
	public void KurseAktualisieren(Kurs data)
	{
			Data.Kurse = null;
			Data.RaisePropertyChanged("Kurse");
	}


Mit Lambda delegates:
    public RegisterMediator()
	{
        //Register a specific delegate to the Mediator
        Mediator.Register("KursAktualisieren",
            (Action<Kurs>)
            delegate(Kurs x) 
            {
                Data.Kurse = null;
				Data.RaisePropertyChanged("Kurse");
            });
    }





Im KursStamm die Nachricht zum Aktualisieren senden wenn ein Kurs gespeichert wird:
	protected override bool OnSave()
	{
		using (new WaitCursor())
		{
			...
			//send the message and the corrent data
			Mediator.NotifyColleagues<Kurs>("KursAktualisieren", Data.SelectedKurs);
		}
	}
