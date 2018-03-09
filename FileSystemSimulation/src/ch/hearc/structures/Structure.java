package ch.hearc.structures;

import java.awt.Graphics;
import java.awt.font.FontRenderContext;
import java.awt.geom.AffineTransform;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

import ch.hearc.Coord;
import ch.hearc.Settings;
import ch.hearc.interfaces.IObjectPaint;

public class Structure implements IObjectPaint {

	private ArrayList<StructureSector> structureSectors;
	private int initX, initY, width, height;
	
	public Structure(int _numberSector, String[] _structureSectors) {
		// Default initX and initY = 5
		this(_numberSector, _structureSectors, 5, 5);
	}
	
	public Structure(int _numberSector, String[] _structureSectors, int _initX, int _initY) {
		this.initX = _initX;
		this.initY = _initY;
		this.width = 0;
		this.height = Settings.STRUCTURE_DEFAULT_HEIGHT;
		this.structureSectors = new ArrayList<StructureSector>(_numberSector);
		this.createStructureSector(_structureSectors);
	}
	
	public void createStructureSector(String[] _structureSectors) {
		int x = initX, y = initY;
		for(String name : _structureSectors) {
			//Calculate string width
			Rectangle2D rect = Settings.FONT_DEFAULT.getStringBounds(name, new FontRenderContext(new AffineTransform(), false, true));
			int strWidth = (int) rect.getWidth();
			Coord coord = new Coord(x, y, strWidth, height);
			this.structureSectors.add(new StructureSector(name, coord));
			x += strWidth;
			width += strWidth;
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
	
	public int getWidth() {
		return initX + width;
	}
	
	public ArrayList<StructureSector> getSectors() {
		return structureSectors;
	}
	
	public void paint(Graphics _g) {
		_g.drawRect(initX, initY, width, height);
		for(StructureSector sector : structureSectors) {
			sector.paint(_g);
		}
	}
	
	public void paint(Graphics _g, int _x, int _y) {
		return;
	}
	
	public boolean intersects(int _x, int _y) {
		return _x > this.initX && _x < this.initX + width && _y > this.initY && _y < this.initY + height;
	}
}
