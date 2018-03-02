package ch.hearc.structures;

import java.awt.Graphics;
import java.util.ArrayList;

import ch.hearc.interfaces.IObjectPaint;

public class Structure implements IObjectPaint {

	private ArrayList<StructureSector> structureSectors;
	
	public Structure(int _numberSector, String[] _structureSectors) {
		this.structureSectors = new ArrayList<StructureSector>(_numberSector);
		this.createStructureSector(_structureSectors);
	}
	
	public void createStructureSector(String[] _structureSectors) {
		for(String _name : _structureSectors) {
			this.structureSectors.add(new StructureSector(_name));
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
		_g.drawRect(0, 0, 500, 40);
		for(StructureSector sector : structureSectors) {
			sector.paint(_g);
		}
	}
}
