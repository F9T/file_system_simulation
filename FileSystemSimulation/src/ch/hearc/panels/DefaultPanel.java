package ch.hearc.panels;

import javax.swing.JPanel;

import ch.hearc.Settings;
import ch.hearc.interfaces.IDisposable;

public class DefaultPanel extends JPanel implements IDisposable {

	private static final long serialVersionUID = -6153327367838181922L;

	public DefaultPanel() {
		this.setBackground(Settings.PANEL_DEFAULT_BACKGROUND_COLOR);
	}
	
	@Override
	public void dispose() {
	}
}
