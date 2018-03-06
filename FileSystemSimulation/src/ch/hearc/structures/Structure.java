package ch.hearc.structures;

import java.awt.Font;
import java.awt.Graphics;
import java.awt.Rectangle;
import java.awt.font.FontRenderContext;
import java.awt.geom.AffineTransform;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

import ch.hearc.Coord;
import ch.hearc.Settings;
import ch.hearc.interfaces.IObjectPaint;

public class Structure implements IObjectPaint {

	private final int DEFAULT_HEIGHT = 40;
	
	private ArrayList<StructureSector> structureSectors;
	private int initX, initY;
	
	public Structure(int _numberSector, String[] _structureSectors) {
		// Default initX and initY = 5
		this(_numberSector, _structureSectors, 5, 5);
	}
	
	public Structure(int _numberSector, String[] _structureSectors, int _initX, int _initY) {
		this.initX = _initX;
		this.initY = _initY;
		this.structureSectors = new ArrayList<StructureSector>(_numberSector);
		this.createStructureSector(_structureSectors);
	}
	
	public void createStructureSector(String[] _structureSectors) {
		int x = initX, y = initY;
		for(String name : _structureSectors) {
			//Calculate string width and height
			Rectangle2D rect = Settings.DEFAULT_FONT.getStringBounds(name, new FontRenderContext(new AffineTransform(), false, true));
			int strWidth = (int) rect.getWidth();
			int strHeight = (int) rect.getHeight();
			Coord coord = new Coord(x, y, strWidth, DEFAULT_HEIGHT);
			this.structureSectors.add(new StructureSector(name, coord));
			x += strWidth;
		}
	}
	
	public void addStructureSector(StructureSector _structureSector) {
		if(_structureSector == null) return;
		this.structureSectors.add(_structureSector);
	}
	
	public void removeStructureSector(int _index) {
		if(_index < 0 || _index >= structureSectors.size()) return;
		this.structureSectors.remove(_index);
	}
	
	public void removeStructureSector(StructureSector _structureSector) {
		if(_structureSector == null) return;
		this.structureSectors.remove(_structureSector);
	}
	
	public void paint(Graphics _g) {
		_g.drawRect(initX, initY, 500, 40);
		for(StructureSector sector : structureSectors) {
			sector.paint(_g);
		}
	}
	
	public void paint(Graphics _g, int _x, int _y) {
		return;
	}
}
