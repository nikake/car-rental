import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { RentCarComponent } from './rent-car.component';

describe('RentCarComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        RentCarComponent
      ],
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(RentCarComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'ClientApp'`, () => {
    const fixture = TestBed.createComponent(RentCarComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('ClientApp');
  });

  it('should render title in a h1 tag', () => {
    const fixture = TestBed.createComponent(RentCarComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Welcome to ClientApp!');
  });
});
