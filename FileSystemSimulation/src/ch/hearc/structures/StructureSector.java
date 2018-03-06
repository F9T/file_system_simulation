package ch.hearc.structures;

import java.awt.Graphics;

import ch.hearc.Coord;
import ch.hearc.Settings;
import ch.hearc.interfaces.IObjectPaint;

public class StructureSector implements IObjectPaint {
	
	private String name;
	private Coord coord;
	
	public StructureSector(String _name, Coord _coord) {
		this.name = _name;
		this.coord = _coord;
	}
	
	public void paint(Graphics _g) {
		_g.drawRect(coord.getX(), coord.getY(), coord.getWidth(), coord.getHeight());
		int strX = coord.getX() + (Settings.PADDING_LEFT * 2);
		int strY = (coord.getY() + Settings.FONT_SIZE + Settings.STRUCTURE_DEFAULT_HEIGHT) / 2;
		_g.drawString(name, strX, strY);
	}
	
	public String getName() {
		return name;
	}
}
