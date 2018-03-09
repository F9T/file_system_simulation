package ch.hearc;

import java.awt.Color;
import java.awt.Font;

public class Settings {

	//General
	public final static int FONT_SIZE = 12;
	public final static String FONT_NAME = "Verdana";
	public final static Font FONT_DEFAULT = new Font(FONT_NAME, Font.BOLD, FONT_SIZE);
	public final static int FRAME_WIDTH = 1024, FRAME_HEIGHT = 800;
	
	//Structure
	public final static int STRUCTURE_DEFAULT_HEIGHT = 50;
	
	//Structure sector
	public final static int SECTOR_PADDING_LEFT = 5;
	public final static Color SECTOR_DEFAULT_BACKGROUND_COLOR = Color.WHITE;
	public final static Color SECTOR_DEFAULT_BORDER_COLOR = Color.BLACK;
	public final static Color SECTOR_DEFAULT_FONT_COLOR = Color.BLACK;
	
	//Panel
	public final static Color PANEL_DEFAULT_BACKGROUND_COLOR = Color.WHITE;
}
