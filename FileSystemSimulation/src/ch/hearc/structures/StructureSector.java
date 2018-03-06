package ch.hearc.structures;

import java.awt.Dimension;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Point;

import ch.hearc.Coord;
import ch.hearc.Settings;
import ch.hearc.interfaces.IObjectPaint;

public class StructureSector implements IObjectPaint {
	
	private final int PADDING_LEFT_RIGHT = 5, PADDING_TOP_BOTTOM = 20;
	
	private String name;
	private Coord coord;
	
	public StructureSector(String _name, Coord _coord) {
		this.name = _name;
		this.coord = _coord;
	}
	
	public void paint(Graphics _g) {
		_g.drawRect(coord.getX(), coord.getY(), coord.getWidth(), coord.getHeight());
		int strX = coord.getX() + PADDING_LEFT_RIGHT;
		int strY = coord.getY() + PADDING_TOP_BOTTOM;
		_g.drawString(name, strX, strY);
	}
	
	public String getName() {
		return name;
	}
}
