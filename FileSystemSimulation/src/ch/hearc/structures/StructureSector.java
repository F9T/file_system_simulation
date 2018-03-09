package ch.hearc.structures;

import java.awt.Color;
import java.awt.Graphics;

import ch.hearc.Coord;
import ch.hearc.Settings;
import ch.hearc.interfaces.IObjectPaint;

public class StructureSector implements IObjectPaint {
	
	private Color backgroundColor;
	private String name;
	private Coord coord;
	
	public StructureSector(String _name, Coord _coord) {
		this.resetColor();
		this.name = _name;
		this.coord = _coord;
	}
	
	public void paint(Graphics _g) {
		_g.setColor(Settings.SECTOR_DEFAULT_BORDER_COLOR);
		_g.drawRect(coord.getX(), coord.getY(), coord.getWidth(), coord.getHeight());
		_g.setColor(backgroundColor);
		_g.fillRect(coord.getX()+1, coord.getY()+1, coord.getWidth()-1, coord.getHeight()-1);
		
		int strX = coord.getX() + (Settings.SECTOR_PADDING_LEFT * 2);
		int strY = (coord.getY() + Settings.FONT_SIZE + Settings.STRUCTURE_DEFAULT_HEIGHT) / 2;
		_g.setColor(Settings.SECTOR_DEFAULT_FONT_COLOR);
		_g.drawString(name, strX, strY);
	}
	
	public void setBackgroundColor(Color _color) {
		this.backgroundColor = _color;
	}
	
	public void resetColor() {
		this.backgroundColor = Settings.SECTOR_DEFAULT_BACKGROUND_COLOR;
	}
	
	public String getName() {
		return name;
	}
	
	public int getX() {
		return coord.getX();
	}
	
	public int getY() {
		return coord.getY();
	}
	
	public int getWidth() {
		return coord.getWidth();
	}
	
	public int getHeight() {
		return coord.getHeight();
	}
	
	public boolean intersects(int _x, int _y) {
		return _x > this.getX() && _x < this.getX() + this.getWidth() && _y > this.getY() && _y < this.getY() + this.getHeight();
	}
}
